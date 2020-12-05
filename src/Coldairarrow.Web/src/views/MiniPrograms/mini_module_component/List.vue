<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button type="primary" icon="plus" @click="hanldleAdd()">新建</a-button>
      <a-button type="primary" icon="edit" @click="handleEdit(selectedRowKeys[0])" :disabled="!(selectedCount() == 1)">修改</a-button>
      <a-button type="primary" icon="minus" @click="handleDelete(selectedRowKeys)" :disabled="selectedCount() ==0"
        :loading="loading">删除</a-button>
      <a-button type="primary" icon="redo" @click="getDataList()">刷新</a-button>
    </div>

    <!--    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="10">
          <a-col :md="4" :sm="24">
            <a-form-item label="查询类别">
              <a-select allowClear v-model="queryParam.condition">
                <a-select-option key="Module_Name">页面名称</a-select-option>
                <a-select-option key="Component_Name">组件名称</a-select-option>
                <a-select-option key="Description">组件描述</a-select-option>
                <a-select-option key="Description">备注</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item>
              <a-input v-model="queryParam.keyword" placeholder="关键字" />
            </a-form-item>
          </a-col>
          <a-col :md="6" :sm="24">
            <a-button type="primary" @click="() => {this.pagination.current = 1; this.getDataList()}">查询</a-button>
            <a-button style="margin-left: 8px" @click="() => (queryParam = {})">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div> -->

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }"
      :bordered="true" size="small">
      <span slot="action" slot-scope="text, record">
        <template>
          <a @click="handleEditTo(record.Id)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :parentObj="this"></edit-form>
  </a-card>
</template>

<script>
  import EditForm from './EditForm'

  const columns = [
    // {
    //   title: '页面ID',
    //   dataIndex: 'Module_Id',
    //   width: '10%'
    // },
    {
      title: '页面名称',
      dataIndex: 'Module_Name',
      width: '10%'
    },
    // {
    //   title: '组件ID',
    //   dataIndex: 'Component_Id',
    //   width: '10%'
    // },
    {
      title: '组件名称',
      dataIndex: 'Component_Name',
      width: '10%'
    },
    {
      title: '组件描述',
      dataIndex: 'Description',
      width: '10%'
    },
    {
      title: '序号',
      dataIndex: 'Sort',
      width: '10%'
    },
    {
      title: '操作',
      dataIndex: 'action',
      scopedSlots: {
        customRender: 'action'
      }
    }
  ]

  export default {
    components: {
      EditForm
    },
    mounted() {
      this.queryParam.condition = 'Module_Id'
      this.queryParam.keyword = this.$route.query.Module_Id || ''
      this.getDataList()
    },
    data() {
      return {
        data: [],
        pagination: {
          current: 1,
          pageSize: 10,
          showTotal: (total, range) => `总数:${total} 当前:${range[0]}-${range[1]}`
        },
        filters: {},
        sorter: {
          field: 'Sort',
          order: 'asc'
        },
        loading: false,
        columns,
        queryParam: {},
        selectedRowKeys: []
      }
    },
    methods: {
      handleTableChange(pagination, filters, sorter) {
        this.pagination = { ...pagination
        }
        this.filters = { ...filters
        }
        this.sorter = { ...sorter
        }
        this.getDataList()
      },
      getDataList() {
        this.selectedRowKeys = []
        this.loading = true
        this.$http
          .post('/MiniPrograms/mini_module_component/GetModuleComponentList', {
            PageIndex: this.pagination.current,
            PageRows: this.pagination.pageSize,
            SortField: this.sorter.field || 'Id',
            SortType: this.sorter.order,
            Search: this.queryParam,
            ...this.filters
          })
          .then(resJson => {
            this.loading = false
            this.data = resJson.Data
            const pagination = { ...this.pagination
            }
            pagination.total = resJson.Total
            this.pagination = pagination
          })
      },
      onSelectChange(selectedRowKeys) {
        this.selectedRowKeys = selectedRowKeys
      },
      selectedCount() {
        return this.selectedRowKeys.length
      },
      hanldleAdd() {
        this.$refs.editForm.openForm()
      },
      handleEdit(id) {
        this.$refs.editForm.openForm(id)
      },
      handleEditTo(id) {
        this.$router.push({
          path: '/MiniPrograms/mini_component/List',
          query: {
            Id: id
          }
        })
      },
      handleDelete(ids) {
        var thisObj = this
        this.$confirm({
          title: '确认删除吗?',
          onOk() {
            return new Promise((resolve, reject) => {
              thisObj.$http.post('/MiniPrograms/mini_module_component/DeleteData', ids).then(resJson => {
                resolve()

                if (resJson.Success) {
                  thisObj.$message.success('操作成功!')

                  thisObj.getDataList()
                } else {
                  thisObj.$message.error(resJson.Msg)
                }
              })
            })
          }
        })
      }
    }
  }
</script>
