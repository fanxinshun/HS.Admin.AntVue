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
                <a-select-option key="Page_Name">页面名称</a-select-option>
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
          <a @click="handleEditTo(record.Page_Id, record.Component_Id, record.Component_Code)">编辑</a>
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

  const columns = [{
      title: '页面名称',
      dataIndex: 'PageRemark',
      width: '10%'
    },
    {
      title: '组件类型',
      dataIndex: 'Component_Name',
      width: '10%'
    },
    {
      title: '组件标记',
      dataIndex: 'Tag',
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
    activated() {
      let temp = this.queryParam
      this.queryParam = []
      this.queryParam.push({
        Condition: 'Page_Id',
        Keyword: this.$route.params.pageId || (temp[0] ? temp[0].Keyword : '')
      }, {
        Condition: 'Parent_Component_Id',
        Keyword: this.$route.params.componentId || (temp[1] ? temp[1].Keyword : '')
      })
      this.getDataList()
    },
    mounted() {

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
        queryParam: [],
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
          .post('/MiniPrograms/mini_page_component/GetModuleComponentList', {
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
        if (!this.queryParam[0].Keyword) {
          this.$message.error('请重新点击编辑页面')
          return
        }
        this.$refs.editForm.openAddForm(this.queryParam[0].Keyword)
      },
      handleEdit(id) {
        this.$refs.editForm.openEditForm(id)
      },
      handleEditTo(pageId, componentId, componentCode) {
        switch (componentCode) {
          case 'swiper': // 轮播图，
          case 'pictures': // 图文广告
            this.$router.push({
              name: '/MiniPrograms/mini_component/swiper/List',
              params: {
                pageId: pageId,
                componentId: componentId
              }
            })
            break
          case 'props': // 商品组，
            this.$router.push({
              name: '/MiniPrograms/mini_component/product/List',
              params: {
                pageId: pageId,
                componentId: componentId
              }
            })
            break
          case 'coms': // 嵌套组件，
            this.$router.push({
              name: '/MiniPrograms/mini_component/nested/List',
              params: {
                pageId: pageId,
                componentId: componentId
              }
            })
            break
          default:
            break
        }
      },
      handleDelete(ids) {
        var thisObj = this
        this.$confirm({
          title: '确认删除吗?',
          onOk() {
            return new Promise((resolve, reject) => {
              thisObj.$http.post('/MiniPrograms/mini_page_component/DeleteData', ids).then(resJson => {
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
